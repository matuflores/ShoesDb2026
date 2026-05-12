using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Common
{
    public class Result
    {
        public bool IsSuccess { get; }//aca se asigna el valor de IsSuccess, y se hace readonly para que no se pueda modificar despues de la creacion del objeto
        public bool IsFailure => !IsSuccess; //aca se asigna el valor de IsFailure, que es el inverso de IsSuccess, y se hace readonly para que no se pueda modificar despues de la creacion del objeto

        public List<string> Errors { get; } = new(); //aca se asigna el valor de Errors, que es una lista de strings vacia, y se hace readonly para que no se pueda modificar despues de la creacion del objeto

        private Result(bool success, List<string> errors)
        {
            IsSuccess = success;
            Errors = errors;
        }

        public static Result Success()
        {
            return new Result(true, new List<string>());
        }//aca se asigna el valor de Success, que es un metodo estatico que devuelve un nuevo objeto Result con IsSuccess = true y Errors = una lista vacia

        public static Result Failure(List<string> errors)
        {
            return new Result(false, errors);
        }//aca se asigna el valor de Failure, que es un metodo estatico que devuelve un nuevo objeto Result con IsSuccess = false y Errors = la lista de errores que se le pasa como parametro

        public static Result Failure(string error)
        {
            return new Result(false, new List<string> { error });
        }//aca se asigna el valor de Failure, que es un metodo estatico que devuelve un nuevo objeto Result con IsSuccess = false y Errors = una lista con un solo error que se le pasa como parametro
    }
}
