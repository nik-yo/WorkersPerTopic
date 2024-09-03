cd worker

dotnet build ./WorkerApp/WorkerApp/WorkerApp.csproj -c Release
docker build --progress=plain -t worker-app:latest .

docker image tag worker-app:latest localhost:5050/worker-app:latest
docker push localhost:5050/worker-app:latest 
# docker build --progress=plain --no-cache -t :latest .

docker pull localhost:5050/worker-app:latest