import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlunosComponent } from './components/alunos/alunos.component';
import { ProfessoresComponent } from './components/professores/professores.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

const routes: Routes = [
  { path: 'alunos', component: AlunosComponent },
  { path: 'professores', component: ProfessoresComponent },
  { path: 'perfil', component: PerfilComponent },
  { path: 'dashboard', component: DashboardComponent },
  //ao não digitar nada
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  //ao digitar algo sem ser as rotas definidas
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
