import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
// tslint:disable: variable-name
// tslint:disable: typedef

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})
export class EventosComponent implements OnInit {
  Eventos: any = [];
  EventosFiltrados: any = [];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;

  _filtroLista = '';
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.EventosFiltrados = this.filtroLista
      ? this.filtrarEventos(this._filtroLista)
      : this.Eventos;
  }

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getEventos();
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.http.get('http://localhost:5000/evento').subscribe(
      (response) => {
        this.Eventos = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  filtrarEventos(filtro: string): any {
    filtro = filtro.toLocaleLowerCase();
    return this.Eventos.filter(
      (evento: { tema: string }) =>
        evento.tema.toLocaleLowerCase().indexOf(filtro) !== -1
    );
  }
}
