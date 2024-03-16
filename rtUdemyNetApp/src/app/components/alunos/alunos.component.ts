import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject} from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Professor } from '../../models/Professor';
import { Aluno } from '../../models/Aluno';
import { ProfessorService } from '../../services/professor.service';
import { AlunoService } from '../../services/aluno.service';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.css']
})
export class AlunosComponent implements OnInit, OnDestroy {

  public modalRef!: BsModalRef;
  public alunoForm!: FormGroup;
  public titulo = 'Alunos';
  public alunoSelecionado!: Aluno | null;
  public textSimple!: string;
  public profsAlunos!: Professor[];

  private unsubscriber = new Subject<void>();


  public alunos!: Aluno[];
  public aluno!: Aluno;
  public msnDeleteAluno!: string;
  public modeSave = 'post';

  constructor(
    private alunoService: AlunoService,
    private route: ActivatedRoute,
    private professorService: ProfessorService,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {
    this.criarForm();
  }

  ngOnInit(): void {
    this.carregarAlunos();
  }

  professoresAlunos(template: TemplateRef<any>, id: number): void {
    this.spinner.show();
    this.professorService.getByAlunoId(id)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((professores: Professor[]) => {
        this.profsAlunos = professores;
        this.modalRef = this.modalService.show(template);
      }, (error: any) => {
        this.toastr.error(`erro: ${error}`);
        console.error(error);
      }, () => this.spinner.hide()
    );
  }

  criarForm(): void {
    this.alunoForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      telefone: ['', Validators.required]
    });
  }

  saveAluno(): void {
    if (this.alunoForm.valid) {
      this.spinner.show();

      if (this.modeSave === 'post') {
        this.aluno = {...this.alunoForm.value};
      } else {
        if (this.alunoSelecionado) {
          this.aluno = {id: this.alunoSelecionado.id, ...this.alunoForm.value};
        }
      }

      const alunoServiceTyped = this.alunoService as unknown as { [key: string]: (aluno: Aluno) => Observable<any> };

      alunoServiceTyped[this.modeSave](this.aluno)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.carregarAlunos();
            this.toastr.success('Aluno salvo com sucesso!');
          }, (error: any) => {
            this.toastr.error(`Erro: Aluno não pode ser salvo!`);
            console.error(error);
          }, () => this.spinner.hide()
        );

    }
  }

  carregarAlunos(): void {
    const id = this.route.snapshot.paramMap?.get('id') ? +this.route.snapshot.paramMap.get('id')! : 0;

    this.spinner.show();
    this.alunoService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe({
        next: (alunos: Aluno[]) => {
          this.alunos = alunos;

          if (id > 0) {
            this.alunoSelect(this.alunos.find(aluno => aluno.id === id) || new Aluno());
          }

          this.toastr.success('Alunos foram carregado com Sucesso!');
        },
        error: (error: any) => {
          this.toastr.error('Alunos não carregados!');
          console.log(error);
        },
        complete: () => this.spinner.hide()
      });
  }

  alunoSelect(aluno: Aluno): void {
    this.modeSave = 'put';
    this.alunoSelecionado = aluno;
    this.alunoForm.patchValue(aluno);
  }

  voltar(): void {
    this.alunoSelecionado = null;
  }

  openModal(template: TemplateRef<any>, alunoId: number): void {
    this.professoresAlunos(template, alunoId);
  }

  closeModal(): void {
    this.modalRef.hide();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

}

