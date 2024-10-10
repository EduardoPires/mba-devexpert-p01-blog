﻿using BlogExpert.Negocio.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogExpert.Dados.Mappings
{
    public class ComentarioMapping : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.HasKey(comentario => comentario.Id);

            builder.Property(comentario => comentario.Descricao)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.Property(comentario => comentario.EmailCriacao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Comentarios");
        }
    }
}
