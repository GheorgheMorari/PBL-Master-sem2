from fastapi import FastAPI

from processing_backend.backend.core.domain.backend_inputs import IngestInput, DeleteInput, QueryInput
from processing_backend.backend.core.entrypoints import PROCESSING_API_ENTRY_POINT_INGEST_IMAGE_PATH, \
    PROCESSING_API_ENTRY_POINT_DELETE_IMAGE_PATH, PROCESSING_API_ENTRY_POINT_QUERY_PATH
from processing_backend.backend.core.services.metadata_services import ingest_images_with_ingest_input, \
    delete_images_with_delete_input, query_images_with_query_input

app = FastAPI()


@app.post(PROCESSING_API_ENTRY_POINT_INGEST_IMAGE_PATH)
def route_ingest_image(ingest_input: IngestInput):
    ingest_images_with_ingest_input(ingest_input)


@app.post(PROCESSING_API_ENTRY_POINT_DELETE_IMAGE_PATH)
def route_delete_image(delete_input: DeleteInput):
    delete_images_with_delete_input(delete_input)


@app.post(PROCESSING_API_ENTRY_POINT_QUERY_PATH, response_model=list[str])
def route_query(query_input: QueryInput):
    return query_images_with_query_input(query_input)