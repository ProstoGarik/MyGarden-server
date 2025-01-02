import json
import os

def list_files_with_relative_path(directory):
    files_list = []
    try:
        for root, dirs, files in os.walk(directory):
            for file in files:
                files_list.append(os.path.join(root, file))
        return files_list
    except Exception as e:
        print(f"An error occurred: {e}")


def deduplicateData(data_path):
    path2 = data_path + "/links/plant_links_data.txt"
    path1 = data_path + "/data/plant_data.json"

    with open(path2,'r',encoding='utf-8') as json_file:
        plants_data = json_file.read().split(',\n')
    
    setted = set(plants_data)

    with open(path2,'w',encoding='utf-8') as json_file:
        json_file.write(",\n".join(setted))


    with open(path1,'r',encoding='utf-8') as json_file:
        plants_data = json.load(json_file)

    unique_data = {json.dumps(item, sort_keys=True): item for item in plants_data}.values()
    result = list(unique_data)
    print(len(plants_data))
    print(len(result))
    with open(path1,'w',encoding='utf-8') as json_file:
        json.dump(result, json_file, ensure_ascii=False, indent=4)

