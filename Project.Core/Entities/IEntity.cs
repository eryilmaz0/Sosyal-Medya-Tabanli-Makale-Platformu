using System;

namespace Project.Core.Entities
{
    public interface IEntity<T>
    {
        //TÜM ENTITYLERDEKİ ORTAK ALANLAR

        T Id { get; set; }
        DateTime Created { get; set; }
    }
}