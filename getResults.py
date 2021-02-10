# /usr/bin/python3.9

import os
import pathlib
import json as json

path = pathlib.Path(__file__).parent.absolute()

resultFolders = list(filter(lambda x: "Results_" in x, os.listdir(path)))

results = {}

for folder in resultFolders:
    results[folder] = {}
    for file in os.listdir(folder):
        if "result_" not in file:
            continue
        fileName = "_".join(filter(lambda x: ".txt" not in x, file.split("_")))
        if fileName not in results[folder]:
            results[folder][fileName] = {}
        content = open(str(path) + "/" + folder + "/" + file)
        lines = list(map(lambda x: x.replace(
            "\n", "").split(", "), content.readlines()))
        content.close()
        for test in lines:
            testResult = test[0].split(": ")
            if testResult[0] not in results[folder][fileName]:
                results[folder][fileName][testResult[0]] = []
            results[folder][fileName][testResult[0]].append(
                float(testResult[1]))
    # Average all results
    for r in results[folder]:
        for res in results[folder][r]:
            val = results[folder][r][res]
            avg = sum(val) / len(val)
            results[folder][r][res] = avg


results = json.dumps(results, indent=2)

f = open("result.json", "w")
f.write(results)
f.close()
