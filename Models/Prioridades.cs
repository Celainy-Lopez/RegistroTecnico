﻿using System.ComponentModel.DataAnnotations;

namespace RegistroTecnico.Models;

public class Prioridades
{
	[Key]
	public int PrioridadId {  get; set; }	

	[Required(ErrorMessage ="Ingrese una descripción valida")]
	public string DescripcionPrioridad { get; set; }

	[Required(ErrorMessage ="Ingrese un tiempo valido")]
	[Range(1, int.MaxValue, ErrorMessage = "El tiempo debe ser mayor que 0")]
    public double? Tiempo { get; set; } 
}