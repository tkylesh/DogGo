using DogGo.Models;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById(int Id);
        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
        void DeleteDog(int DogId);
        List<Dog> GetDogsByOwnerId(int ownerId);
    }
}
