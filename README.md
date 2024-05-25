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


TODOs:
- [ ] Create a common api for the backend
- [ ] Create a structure for the metadata database
- [ ] Create a structure for the content database
- [ ] Implement Website interface
- [ ] Implement Mobile app interface
- [ ] Implement Common backend
- [ ] Implement Processing backend


## Common infrastructure
- `make start-common-infrastructure` to start
- `make stop-common-infrastructure` to stop

### Processing backend
- Depends on common infrastructure
- `make build-processing-backend` to build
- `make start-processing-backend` to start
- `make stop-processing-backend` to stop
- http://image-captioning-service.localtest.me/docs for captioning service
