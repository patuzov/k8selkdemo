# Install
## Add helm repositories
`helm repo add elastic https://helm.elastic.co`
`helm repo update`

## Elasticsearch
`helm install elasticsearch elastic/elasticsearch --set replicas=2 --set minimumMasterNodes=1`

## Kibana
`helm install kibana elastic/kibana --set service.type=LoadBalancer`

## Filebeat
`helm install filebeat elastic/filebeat -f fbvalues.yaml`

## Jaeger
`helm repo add jaegertracing https://jaegertracing.github.io/helm-charts`
`helm install jaeger jaegertracing/jaeger --set query.service.type=LoadBalancer`
