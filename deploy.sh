docker build -t localhost/steps-ui:latest ./steps-ui --build-arg REACT_APP_API_URL=http://steps.homelab.io/api
docker build -t localhost/steps-api:latest ./Steps.Api
docker save localhost/steps-ui:latest -o steps-ui.tar
docker save localhost/steps-api:latest -o steps-api.tar
k3s ctr images import steps-ui.tar 
k3s ctr images import steps-api.tar 
kubectl rollout restart deployment steps-ui
kubectl rollout restart deployment steps-api 