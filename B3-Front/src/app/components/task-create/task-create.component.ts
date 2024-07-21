import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from '../../services/task.service';


@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrl: './task-create.component.css'
})
export class TaskCreateComponent {
  task = {
    description: '',
    status: 1
  };

  loading = false; // Inicializa o estado de carregamento

  constructor(private router: Router, private taskService: TaskService) { }


  async onSubmit() {
    this.loading = true; // Ativa o indicador de carregamento

    this.taskService.AddTask(this.task);
    console.log(this.task);
    await new Promise(resolve => setTimeout(resolve, 1000));

    this.router.navigate(['/list']);
  }
  
}
