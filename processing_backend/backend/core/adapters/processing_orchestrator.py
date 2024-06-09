from threading import Thread


class ProcessingOrchestrator:
    orchestrator_thread = None
    stop_flag = False

    @classmethod
    def start(cls, target):
        cls.orchestrator_thread = Thread(target=target)
        cls.orchestrator_thread.start()

    @classmethod
    def stop(cls):
        cls.stop_flag = True
        cls.orchestrator_thread.join()

    @classmethod
    def is_stopped(cls):
        return cls.stop_flag
