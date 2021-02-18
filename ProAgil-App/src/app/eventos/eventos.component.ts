import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})

export class EventosComponent implements OnInit {

  eventos!: Evento[];
  eventosFiltrados!: Evento[];
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  modalRef!: BsModalRef;
  registerForm!: FormGroup;
  
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

  constructor(private eventoService: EventoService, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.validation();
    this.getEventos();
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  validation() {
    this.registerForm = new FormGroup({
      tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      local: new FormControl('', Validators.required),
      dataEvento: new FormControl('', Validators.required),
      qtdPessoas: new FormControl('', [Validators.required, Validators.max(120000)]),
      imagemURL: new FormControl('', Validators.required),
      telefone: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  salvarAlteracao() {

  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
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
}
