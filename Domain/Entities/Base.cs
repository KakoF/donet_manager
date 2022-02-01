using System;

namespace Domain.Entities
{
    public class Base
    {
        

        //Por default, usar propriedades private SET, e inicializá-las no construtor para que não seja posssível alterar dados fixos da instância
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }
        public Base(){}
        public Base(int id, DateTime dataCriacao, DateTime? dataAtualizacao)
        {
            Id = id;
            DataCriacao = dataCriacao;
            DataAtualizacao = dataAtualizacao;
        }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
