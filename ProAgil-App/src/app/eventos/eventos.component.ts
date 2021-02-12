import { Component, OnInit } from '@angular/core';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

// tslint:disable: variable-name
// tslint:disable: typedef

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})

export class EventosComponent implements OnInit {

  eventos: Evento[] = [];
  eventosFiltrados: Evento[] = [];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;

  _filtroLista = '';
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista
      ? this.filtrarEventos(this._filtroLista)
      : this.eventos;
  }

  constructor(private eventoService: EventoService) {}

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.eventoService.getEventos().subscribe(
      (eventos: Evento[]) => {
        this.eventos = eventos;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  filtrarEventos(filtro: string): Evento[] {
    filtro = filtro.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string }) =>
        evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }
}
