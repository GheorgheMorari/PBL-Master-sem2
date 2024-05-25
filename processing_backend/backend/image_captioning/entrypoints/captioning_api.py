import http

from fastapi import FastAPI, HTTPException

from processing_backend.backend.core.domain.image_file import B64ImageFile
from processing_backend.backend.image_captioning.services import CAPTIONING_API_ENTRY_POINT_PREDICT_CAPTION_PATH
from processing_backend.backend.image_captioning.services.image_captioning import image_captioning, \
    init_captioning_model

app = FastAPI(on_startup=[init_captioning_model])


@app.post(CAPTIONING_API_ENTRY_POINT_PREDICT_CAPTION_PATH, response_model=str)
def route_predict_caption(b64_image_file: B64ImageFile) -> str:
    try:
        return image_captioning(b64_image_file)
    except Exception as exception:
        raise HTTPException(status_code=http.HTTPStatus.INTERNAL_SERVER_ERROR, detail=f"Exception:{exception}")
