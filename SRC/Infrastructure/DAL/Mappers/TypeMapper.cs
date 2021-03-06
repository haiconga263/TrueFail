﻿using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL.Mappers
{
    public static class TypeMapper
    {
        public static void Initialize(IEnumerable<Assembly> assemblies)
        {
            List<Type> types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                types.AddRange((from type in assembly.GetTypes()
                                where type.IsClass && (type.Namespace.Contains("Models") || type.Namespace.Contains("ViewModels"))
                                        && type.CustomAttributes.Any(a => a.AttributeType == typeof(TableAttribute))
                                select type));
            }

            types.ToList().ForEach(type =>
            {
                var mapper = (SqlMapper.ITypeMap)Activator
                    .CreateInstance(typeof(ColumnAttributeTypeMapper<>)
                                    .MakeGenericType(type));
                SqlMapper.SetTypeMap(type, mapper);
            });
        }

        public static void Initialize(Assembly assembly)
        {
            List<Type> types = new List<Type>();

            types.AddRange((from type in assembly.GetTypes()
                            where type.IsClass && (type.Namespace.Contains("Models") || type.Namespace.Contains("ViewModels"))
                                  && type.CustomAttributes.Any(a => a.AttributeType ==  typeof(TableAttribute))
                            select type));

            types.ToList().ForEach(type =>
            {
                var mapper = (SqlMapper.ITypeMap)Activator
                    .CreateInstance(typeof(ColumnAttributeTypeMapper<>)
                                    .MakeGenericType(type));
                SqlMapper.SetTypeMap(type, mapper);
            });
        }
    }
}
