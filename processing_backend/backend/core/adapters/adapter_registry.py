from typing import Type, Optional, Callable, Any


class AdapterRegistry:
    adapter_registry: dict = {}

    @classmethod
    def register(cls, adapter_type: Type, adapter: object) -> None:
        cls.adapter_registry[str(adapter_type)] = adapter

    @classmethod
    def unregister_adapter(cls, repository_type: Type) -> None:
        del cls.adapter_registry[str(repository_type)]

    @classmethod
    def get_adapter(cls, repository_type: Type, default_adapter_retriever: Optional[Callable] = None) -> Any:
        repository = cls.adapter_registry.get(str(repository_type), None)
        if repository is None:
            if default_adapter_retriever is not None:
                repository = default_adapter_retriever()
                cls.register(adapter_type=repository_type, adapter=repository)
            else:
                raise Exception(f"Adapter not found for {repository_type} and no default retriever provided.")
        return repository
