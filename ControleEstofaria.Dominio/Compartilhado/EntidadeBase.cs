﻿using Taikandi;

namespace ControleEstofaria.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        //public Guid UsuarioId { get; set; }

        //public Usuario Usuario { get; set; }


        public EntidadeBase()
        {
            Id = SequentialGuid.NewGuid();
        }

        public abstract void Atualizar(T registro);
    }
}
