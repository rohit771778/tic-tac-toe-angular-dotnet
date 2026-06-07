import { Component } from '@angular/core';
import { GameComponent } from './pages/game/game';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [GameComponent],
  templateUrl: './app.html',
 styleUrls: ['./app.scss']
})
export class App {
}