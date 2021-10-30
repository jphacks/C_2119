import cv2
import numpy as np
import boto3
import time
import json
from decimal import Decimal
import math

class ContourDetection:
  def detect_text(self, photo, bucket):
      client=boto3.client('rekognition')
      response=client.detect_text(Image={'S3Object':{'Bucket':bucket ,'Name':photo}})                 
      textDetections=response['TextDetections']
      print ('Detected text\n----------')
      for text in textDetections:
              print ('Detected text:' + text['DetectedText'])
              print ('Confidence: ' + "{:.2f}".format(text['Confidence']) + "%")
              print ('Id: {}'.format(text['Id']))
              if 'ParentId' in text:
                  print ('Parent Id: {}'.format(text['ParentId']))
              print ('Type:' + text['Type'])
              print()
      return textDetections

  def main(self, file_path, file_name):
    dynamo = boto3.resource('dynamodb')
    table = dynamo.Table("jphacks2021")
    img_color = cv2.imread(file_path)
    img_width = img_color.shape[1]
    img_height = img_color.shape[0]
    # img = cv2.imread("../madorizu/5.png", 0)
    img = cv2.imread(file_path, 0)
    ret, img_thresh = cv2.threshold(img, 0, 255, cv2.THRESH_OTSU)
    cv2.imwrite("detected/image_thresh.png", img_thresh)
    gray = cv2.bitwise_not(img_thresh)
    contours = cv2.findContours(gray, cv2.RETR_TREE, cv2.CHAIN_APPROX_NONE)[0]
    area_thresh = 2000
    img_contour = img_color
    contours = list(filter(lambda x: cv2.contourArea(x) > area_thresh, contours))
    count_contour = 0
    for cnt in contours:
        count_contour += 1
        # 輪郭に外接する長方形を取得する。
        x, y, width, height = cv2.boundingRect(cnt)
        # 描画する。
        cv2.rectangle(img_contour, (x, y), (x + width, y + height), color=(0, 255, 0), thickness=2)
        # img_contour = cv2.circle(img_contour, (x, y), 10, (255, 0, 0), 2)
    print(count_contour)
    # cv2.imshow('image', img_contour)
    # cv2.waitKey(1000)
    textDetections = self.detect_text(file_name, "jphacks2021-c-2119")
    area_list = []
    size_rooms = {}
    for text in textDetections:
      if (text['Type'] == "LINE"):
        print ('Detected text:' + text['DetectedText'])
        print ('Geometry_Top : ' + "{:.5f}".format(text['Geometry']['BoundingBox']["Top"]*img_height))
        print ('Geometry_Left: ' + "{:.5f}".format(text['Geometry']['BoundingBox']["Left"]*img_width))
        area = [float(text['DetectedText'])*1.62, text['Geometry']['BoundingBox']["Top"]*img_height, text['Geometry']['BoundingBox']["Left"]*img_width]
        area_list.append(area)
        size_rooms[area[0]] = [-1, -1, -1, -1]
    # size_rooms = np.zeros((len(area_list), 5))
    # size_rooms.fill(-1)
    for cnt in contours:
        x, y, width, height = cv2.boundingRect(cnt)
        for each_area in area_list:
          if(x < each_area[2] and x+width > each_area[2] and y < each_area[1] and y+height > each_area[1]):        
            if (size_rooms[each_area[0]][0] < 0):
              size_rooms[each_area[0]] = [x, y, width, height]
              print(x, y, width, height)
              print(each_area)
            else:
              if(width*height < size_rooms[each_area[0]][2] * size_rooms[each_area[0]][3]):
                size_rooms[each_area[0]] = [x, y, width, height]
                print(x, y, width, height)
                print(each_area)
                cv2.rectangle(img_contour, (x, y), (x + width, y + height), color=(255, 0, 0), thickness=2)
    scale = math.sqrt(size_rooms[area_list[0][0]][2] * size_rooms[area_list[0][0]][3] / area_list[0][0])
    lines = []
    for cnt in contours:
        x, y, width, height = cv2.boundingRect(cnt)
        new_x = str('{:.5f}'.format((x / scale) * 5))
        new_y = str('{:.5f}'.format((y / scale) * 5))
        new_width = str('{:.5f}'.format((width / scale) * 5))
        new_height = str('{:.5f}'.format((height / scale) * 5))
        # new_x = str('{:.1f}'.format(x))
        # new_y = str('{:.1f}'.format(y))
        # new_width = str('{:.1f}'.format(width))
        # new_height = str('{:.1f}'.format(height))
        cv2.rectangle(img_contour, (int(x*10 / scale), int(y*10 / scale)), (int((x+width)*10/scale), int((y+height)*10/scale)), color=(255, 0, 0), thickness=2)
        lines.append([new_x, new_y, new_width, new_height])
        
    response = table.put_item(
        Item={
              'fileName': file_name,
              'time': Decimal(time.time()),
              'data': lines
          }
    )
    cv2.imwrite("detected/image5.png", img_color)

if __name__ == "__main__":
  main()