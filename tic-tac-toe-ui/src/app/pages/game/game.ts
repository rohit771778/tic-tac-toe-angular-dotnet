import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameService } from '../../services/game.service';
import { ScoreboardService } from '../../services/scoreboard.service';

@Component({
  selector: 'app-game',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './game.html',
styleUrls: ['./game.scss']
})
export class GameComponent implements OnInit {

  private gameService = inject(GameService);
  private scoreboardService = inject(ScoreboardService);
  scoreboard: any;

  game: any;

ngOnInit(): void {
  this.createGame();
  this.loadScoreboard();
}

createGame() {
  this.gameService.createGame(0)
    .subscribe({
      next: (res) => {
        console.log('API RESPONSE', res);
        this.game = res;
      },
      error: (err) => {
        console.error(err);
        alert('API Failed');
      }
    });
}
  makeMove(row: number, column: number) {

    if (!this.game)
      return;

    this.gameService
      .makeMove(
        this.game.id,
        this.game.currentPlayer,
        row,
        column
      )
      .subscribe(res => {
        this.game = res;
      });
  }

resetGame() {
  if (!this.game) return;

  this.gameService
    .reset(this.game.id)
    .subscribe({
      next: (res) => {
        console.log('RESET', res);
        this.game = res;
      },
      error: (err) => console.error(err)
    });
}

  undo() {
    this.gameService
      .undo(this.game.id)
      .subscribe(res => {
        this.game = res;
      });
  }

  loadScoreboard() {
  this.scoreboardService
    .getScoreboard()
    .subscribe(res => {
      this.scoreboard = res;
    });
}

resetScoreboard() {
  this.scoreboardService
    .resetScoreboard()
    .subscribe(() => {
      this.loadScoreboard();
    });
}
}