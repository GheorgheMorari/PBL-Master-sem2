from typing import Type, Optional, Callable, Any


class RepositoryRegistry:
    repository_registry: dict = {}

    @classmethod
    def register_repository(cls, repository_type: Type, repository: object) -> None:
        cls.repository_registry[str(repository_type)] = repository

    @classmethod
    def unregister_repository(cls, repository_type: Type) -> None:
        del cls.repository_registry[str(repository_type)]

    @classmethod
    def get_repository(cls, repository_type: Type, default_repository_retriever: Optional[Callable] = None) -> Any:
        repository = cls.repository_registry.get(str(repository_type), None)
        if repository is None:
            if default_repository_retriever is not None:
                repository = default_repository_retriever()
                cls.register_repository(repository_type=repository_type, repository=repository)
            else:
                raise Exception(f"Repository not found for {repository_type} and no default retriever provided.")
        return repository
