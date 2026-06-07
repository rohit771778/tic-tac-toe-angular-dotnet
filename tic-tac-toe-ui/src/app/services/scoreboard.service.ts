import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ScoreboardService {

  private http = inject(HttpClient);

  private apiUrl = 'http://localhost:5084/api/scoreboard';

  getScoreboard() {
    return this.http.get<any>(this.apiUrl);
  }

  resetScoreboard() {
    return this.http.post(this.apiUrl + '/reset', {});
  }
}