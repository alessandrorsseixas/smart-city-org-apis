# Smart City - Local services & URLs

Este README lista as URLs e portas exposas pelo `docker-compose.yml` e instruções rápidas para acessar os serviços via Kubernetes (manifests em `k8s/`).

OBS: este ambiente é para estudo/local. Secrets nos manifests `k8s/` são placeholders e devem ser substituídos por valores reais (base64) antes de aplicar em produção.

## Como subir localmente (Docker Compose)

No root do repositório:

```bash
docker compose up --build
```

APIs (health endpoints disponíveis em `/health`):

- Device Management
  - URL: http://localhost:5001/
  - Health: http://localhost:5001/health
- Energy Monitor
  - URL: http://localhost:5002/
  - Health: http://localhost:5002/health
- Dashboard API
  - URL: http://localhost:5003/
  - Health: http://localhost:5003/health

Infra (composed services)

- Redis
  - Host: localhost:6379
- MQTT (Mosquitto)
  - Broker TCP: tcp://localhost:1883
  - Websocket: ws://localhost:9001
- RabbitMQ
  - AMQP: amqp://localhost:5672
  - Management UI: http://localhost:15672 (user/password configurados no compose)
- Keycloak (dev)
  - URL: http://localhost:8080
- MongoDB
  - URL: mongodb://localhost:27017
- InfluxDB
  - URL: http://localhost:8086
- Kong (DB-less)
  - Proxy: http://localhost:8000
  - Admin API: http://localhost:8001
  - Admin GUI / Manager: http://localhost:8001  # Note: the Kong Manager GUI is an enterprise feature; the Admin API is available at this URL for DB-less configuration
  - Proxy HTTPS: https://localhost:8443
- Kong Management UI (open-source via Konga)
  - URL: http://localhost:1337
  - Notes: Konga is an open-source admin UI for Kong. After opening Konga you can register the Kong Admin endpoint (http://kong:8001) inside the Konga UI to manage services/routes.
- MySQL
  - Host: localhost:3306
- n8n
  - URL: http://localhost:5678

## Manifests Kubernetes (GitOps)

Path principal: `k8s/`

Exemplos criados:

- `k8s/device-management/*`
- `k8s/energy-monitor/*`
- `k8s/dashboard-api/*`
- `k8s/n8n/*` (n8n com SQLite, PVC, Secret placeholder)

Como aplicar (exemplo n8n):

```bash
# ASSUMA que você substituiu os placeholders base64 em k8s/n8n/secret.yaml
kubectl apply -f k8s/n8n/pvc.yaml
kubectl apply -f k8s/n8n/secret.yaml
kubectl apply -f k8s/n8n/deployment.yaml

# Acessar via port-forward
kubectl port-forward deployment/n8n 5678:5678
# abrir http://localhost:5678
```

Para as APIs (exemplo device-management):

```bash
kubectl port-forward deployment/device-management 5001:80
curl http://localhost:5001/health
```

Se desejar expor via Kong (DB-less), crie um `Service` e uma rota no Kong Admin API (http://kong:8001) apontando para o `Service` do Kubernetes.

## Notas importantes

- Secrets: nos arquivos `k8s/*/secret.yaml` há placeholders como `PLACEHOLDER_BASE64_*`. Substitua com `echo -n 'valor' | base64` antes de aplicar.
- SQLite (n8n): bom para estudos; não use em produção. Para produção prefira Postgres (posso adicionar manifests se quiser).
- Istio/Kiali: deixei placeholders no `docker-compose.yml`. Para ambiente Kubernetes, instale Istio via `istioctl install` e Kiali via operador. Não é reproduzível via docker-compose.

## Ajuda / Próximos passos

Posso:

- Gerar `Service` + `Ingress` (ou rota Kong) para o n8n e para as APIs.
- Adicionar probes (readiness/liveness) e limits/resources nos `Deployment`.
- Criar um `README.md` com instruções passo-a-passo para GitOps/ArgoCD.

----
Gerado automaticamente — atualize conforme sua infraestrutura.
