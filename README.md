# Google photos thing
![masters-sem-2.drawio.png](/resources%2Fmasters-sem-2.drawio.png)

# Project Responsibility
### Puscas Dumitru:
- Website interface
- Common backend
### Bogdan Piciriga
- Authentication
- Content database
### Pavel Cerlat:
- Mobile app interface
### Morari Gheorghe:
- Processing backend
- Processing components
- Metadata database

# Views:
1) Auth view
2) List view
3) Single image view

# Functionality
## Web/APP Interface
1) Upload images
2) Get uid for each images
3) Delete images locally
4) Download again
5) Type query -> Receive uuid
6) Show only the filtered images (based on uuid)
## Common Backend
1) Auth
2) For user receive images -> save to s3, ingest post to processing backend
3) Send images for user
4) Forward query to processing backend
## Processing backend
1) Ingest images
2) Orchestrate tasks
3) Call processing microservices
4) Update metadata or content
5) Filter images based on keywords

# Deployment
## Common infrastructure
Mongodb, postgres
- `make start-common-infrastructure` to start
- `make stop-common-infrastructure` to stop

## Processing backend
- Depends on common infrastructure
- `make build-processing-backend` to build
- `make start-processing-backend` to start
- `make stop-processing-backend` to stop


# Documentation


## Processing backend
- http://image-captioning-service.localtest.me/docs
- http://image-compressor-service.localtest.me/docs
- http://image-processing-service.localtest.me/docs

1) http://image-processing-service.localtest.me/api/v1/ingest
`{
    "image_ids" : ["image_uuid1"],
    "user_ids" : ["user_uuid1"]
}`

2) http://image-processing-service.localtest.me/api/v1/delete
`{
    "image_ids" : ["image_uuid1"],
}`

3) http://image-processing-service.localtest.me/api/v1/query
`{
    "user_id" : "user_uuid1",
    "query_string": "cats bed tree"
}`
