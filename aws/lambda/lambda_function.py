import boto3
import json
from boto3.dynamodb.conditions import Key
from decimal import Decimal

print('Loading function')
dynamo = boto3.resource('dynamodb')

def decimal_default_proc(obj):
    print(obj)
    for each_obj in obj:
        if isinstance(each_obj, Decimal):
            return float(each_obj)
    raise TypeError

def lambda_handler(event, context):
    print("Received event: " + json.dumps(event, indent=2))
    print(event['httpMethod'])
    print(event['body'])

    operation = event['httpMethod']
    if operation == 'GET':
        table = dynamo.Table("jphacks2021")
        result = table.scan()
        print(result["Items"])
        #data = result["Items"][0]["data"]
        json_data = result["Items"]
        data = []
        for each_json in json_data:
            each_data = {}
            print(each_json["fileName"])
            each_data["fileName"] = each_json["fileName"]
            each_data["data"] = each_json['data']
            data.append(each_data)
        print(data)
        #payload = event['queryStringParameters']
        message = {
           'message': 'Execution started successfully!'
        }
        return{
            "statusCode": 200,
            'headers': {'Content-Type': 'application/json'},
            'body': json.dumps({"data": data})
        }
