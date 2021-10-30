import cv2
import numpy as np
import time
from decimal import Decimal
import boto3
import json

class LineRecognition:
  def main(self, path, name):
    dynamo = boto3.resource('dynamodb')
    table = dynamo.Table("jphacks_lines")
    img_color = cv2.imread(path)
    img = cv2.imread(path, 0)
    ret, img_thresh = cv2.threshold(img, 0, 255, cv2.THRESH_OTSU)
    gray = cv2.bitwise_not(img_thresh)
    lines = cv2.HoughLinesP(gray, rho=1, theta=np.pi/360, threshold=80, minLineLength=40, maxLineGap=5)
    for line in lines:
      x1, y1, x2, y2 = line[0]

      # 赤線を引く
      img_color = cv2.line(img_color, (x1,y1), (x2,y2), (0,0,255), 3)
    line_data = {"lines" : lines}
    response = table.put_item(
        Item={
              'fileName': name,
              'time': Decimal(time.time()),
              'data': str(lines)
          }
    )
    cv2.imwrite("detected/image.png", img_color)