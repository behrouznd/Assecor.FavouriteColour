namespace Contracts.People;

public interface IPersonRepositoryFactory
{
    IPersonRepository GetPersonRepository();
}
