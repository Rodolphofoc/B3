import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskCreateComponent } from './components/task-create/task-create.component';
import { TaskEditComponent } from './components/task-edit/task-edit.component';
import { TaskListComponent } from './components/task-list/task-list.component';

const routes: Routes = [
  {
    path:"",
    pathMatch:"full",
    redirectTo : "list"
  },
  {
    path:"list", component:TaskListComponent
  },
  {
    path:"edit/:id", component:TaskEditComponent
  },
  {
    path:"add", component:TaskCreateComponent
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
