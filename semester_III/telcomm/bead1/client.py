import sys
import os
import json

if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("Missing input file")
        exit(1)
    filePath = sys.argv[1]
    print(filePath)
    # filePath = "cs1.json"
    if not os.path.exists(filePath):
        print(f"Non-existent file: {filePath}")
        exit(1)

    json_data = open(filePath, 'r').read()
    data = json.loads(json_data)

    duration = int(data['simulation']['duration'])
    demands = data['simulation']['demands']  #type: []
    event = 1
    for i in range(1, duration + 1):
        for demand in demands:
            endpoints = demand["end-points"]
            if int(demand["start-time"]) == i:
                print(f'{event}. igény foglalás: {endpoints[0]} <-> {endpoints[1]} st:{i} - sikeres')
                event += 1
            if int(demand["end-time"]) == i:
                print(f'{event}. igény felszabadítás: {endpoints[0]} <-> {endpoints[1]} st:{i} - sikeres')
                event += 1

