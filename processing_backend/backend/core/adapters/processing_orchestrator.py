from threading import Thread

from processing_backend.backend.core.services.process_orchestrator import process_orchestrator


class ProcessingOrchestrator:
    orchestrator_thread = None
    stop_flag = False

    @classmethod
    def start(cls):
        cls.orchestrator_thread = Thread(target=process_orchestrator)
        cls.orchestrator_thread.start()

    @classmethod
    def stop(cls):
        cls.stop_flag = True
        cls.orchestrator_thread.join()

    @classmethod
    def is_stopped(cls):
        return cls.stop_flag
