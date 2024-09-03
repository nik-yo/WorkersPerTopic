winget install Kubernetes.minikube
winget install Kubernetes.kubectl
winget install Helm.Helm

# dev
helm create worker-chart

# Run local registry
# https://hub.docker.com/_/registry
# docker run -d -p 5050:5000 --restart always --name registry registry:2
