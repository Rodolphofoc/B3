import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { Observable, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class TaskService {

  private readonly baseUrl = "https://localhost:7104/api/B3/";;


  constructor(private http: HttpClient) { }

  GetTasks() {
    var result = this.http.get<any>(this.baseUrl + "TaskManager")
      .pipe(
          map(
              response => { return response.data; })
            );
    console.log(result);
    return result;
  }

  async GetTaskById(id: any) {
    const response = await this.http.get<any>(this.baseUrl + "TaskManager/" + id).toPromise();
    console.log(response);
    return response.data;
  }

  AddTask(task: any) {
    return fetch(this.baseUrl + "TaskManager", {
      method: 'POST',
      body: JSON.stringify(task),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => response.json());
  }


  UpdateTask(task: any) {
    return fetch(this.baseUrl + "TaskManager/" + task.integrationId, {
      method: 'PUT',
      body: JSON.stringify(task),
      headers: {
        'Content-Type': 'application/json'
      }
    }).then(response => response.json());
  }

  DeleteTask(integrationId: string) {
    return fetch(this.baseUrl + "TaskManager/" + integrationId, {
      method: 'DELETE'
    }).then(response => response.json());
  }

}
