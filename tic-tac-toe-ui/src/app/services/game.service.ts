import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private http = inject(HttpClient);

  private apiUrl = 'http://localhost:5084/api/games';

  createGame(mode: number) {
    return this.http.post<any>(
      this.apiUrl,
      { mode }
    );
  }

  makeMove(id: string, player: number, row: number, column: number) {
    return this.http.post<any>(
      `${this.apiUrl}/${id}/moves`,
      {
        player,
        row,
        column
      }
    );
  }

  undo(id: string) {
    return this.http.post<any>(
      `${this.apiUrl}/${id}/undo`,
      {}
    );
  }

  reset(id: string) {
    return this.http.post<any>(
      `${this.apiUrl}/${id}/reset`,
      {}
    );
  }
}