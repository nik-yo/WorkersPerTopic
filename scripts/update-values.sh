#!/bin/bash
app_root_dir=/mnt/c/Projects/WorkersPerTopic/worker/WorkerApp/WorkerApp/bin/Release/net8.0
chart_root_dir=/mnt/c/Projects/WorkersPerTopic/chart/worker-chart

sudo apt update && sudo apt install -y jq
sudo snap install yq

cat $app_root_dir/appsettings.json | jq '.Kafka.Topics | unique_by({Topic}) | {Kafka:{Topics: .}}' | yq -P | idPath=".Topic" originalPath=".Kafka.Topics" yq eval-all '(
  (( (eval(strenv(originalPath))) | .[] | {(eval(strenv(idPath))): .}) as $item ireduce ({}; . * $item )) as $uniqueMap 
  | ( $uniqueMap | to_entries | .[]) as $item ireduce([]; . + $item.value)) as $mergedArray 
  | select(fi == 1) | (eval(strenv(originalPath))) = $mergedArray' - $chart_root_dir/values.yaml | tee values.yaml

echo "`jq '.Kafka.Topics = []' $app_root_dir/appsettings.json`" > $app_root_dir/appsettings.json

# cp values.yaml $chart_root_dir/values.yaml