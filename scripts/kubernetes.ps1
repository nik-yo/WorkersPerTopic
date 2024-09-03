minikube start

# Set docker env
eval $(minikube docker-env)             # Linux
& minikube -p minikube docker-env --shell powershell | Invoke-Expression # PowerShell

cd C:\Projects\WorkersPerTopic\worker

# Single
dotnet build ./WorkerApp/WorkerApp/WorkerApp.csproj -c Release
docker build --progress=plain -t worker-app:latest .

kubectl run worker-app --image=worker-app:latest --image-pull-policy=Never --env="Kafka__BootstrapServers=host.minikube.internal:19094"

# Dynamic 
dotnet build ./WorkerApp/WorkerApp/WorkerApp.csproj -c Release

## Run from WSL
cd /mnt/c/Projects/WorkersPerTopic/worker
../scripts/update-values.sh

docker build --progress=plain -t worker-app:latest .

helm install -f values.yaml worker-app ../chart/worker-chart
# helm upgrade -f values.yaml worker-app ../chart/worker-chart

minikube dashboard

# kubectl apply -f C:\Projects\WorkersPerTopic\manifest\worker-app.yaml

minikube stop

# Misc
docker image ls
docker image rm image_id
kubectl delete --all deployments --namespace=default
docker build --progress=plain --no-cache -t worker-app:latest .
kubectl exec worker-app-fruits-6fdbfbbbf9-7mx4f -- cat appsettings.json
helm uninstall worker-app