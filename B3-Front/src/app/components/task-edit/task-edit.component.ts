import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TaskManager } from '../../models/taskmanager';


@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrl: './task-edit.component.css'
})
export class TaskEditComponent implements  OnInit {

  task = {
    description: '',
    status: 1
  };

  loading = false;
  statusDescriptions: { [key: number]: string } = {
    1: 'Pending',
    2: 'In Progress',
    3: 'Completed'
  };

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router
  ) { }


  ngOnInit(): void {
    this.loadTask();
  }

  async loadTask() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.task = await this.taskService.GetTaskById(id);
      this.loading = false;
    }
  }


  async onSubmit() {
    this.loading = true;
    await new Promise(resolve => setTimeout(resolve, 1000));

    await this.taskService.UpdateTask(this.task);
    this.loading = false;
    this.router.navigate(['/list']); // Navega para a lista de tarefas
  }

  getStatusDescription(status: number): string {
    return this.statusDescriptions[status] || 'Unknown';
  }
}
