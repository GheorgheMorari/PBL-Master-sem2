import http

from fastapi import FastAPI, HTTPException

from processing_backend.backend.core.domain.image_file import B64ImageFile
from processing_backend.backend.image_compressor.entrypoints import COMPRESSOR_API_ENTRY_POINT_PREDICT_CAPTION_PATH
from processing_backend.backend.image_compressor.services.image_compressor import compress_image

app = FastAPI()


@app.post(COMPRESSOR_API_ENTRY_POINT_PREDICT_CAPTION_PATH, response_model=B64ImageFile)
def route_predict_caption(b64_image_file: B64ImageFile) -> B64ImageFile:
    try:
        return compress_image(b64_image_file)
    except Exception as exception:
        raise HTTPException(status_code=http.HTTPStatus.INTERNAL_SERVER_ERROR, detail=f"Exception:{exception}")
