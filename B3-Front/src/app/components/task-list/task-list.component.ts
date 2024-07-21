import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { TaskManager } from '../../models/taskmanager';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent implements OnInit{
  constructor(private taskService: TaskService, private router: Router) { }

tasks: TaskManager[] = [];
statusDescriptions: { [key: number]: string } = {
  1: 'Pending',
  2: 'In Progress',
  3: 'Completed'
};


  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.GetTasks().subscribe(
      (data: TaskManager[]) => {
        this.tasks = data;
      },
      (error) => {
        console.error('Erro ao carregar tarefas', error);
      }
    );
  }

  addTask() {
    this.router.navigate(['/add']);
  }

  editTask(integrationId : string) {
    this.router.navigate(['/task-edit', integrationId]);
  }

  deleteTask(integrationId : string) {
    this.taskService.DeleteTask(integrationId).then((data) => {
      this.loadTasks();
    });
  }

  getStatusDescription(status: number): string {
    return this.statusDescriptions[status] || 'Unknown';
  }
}
