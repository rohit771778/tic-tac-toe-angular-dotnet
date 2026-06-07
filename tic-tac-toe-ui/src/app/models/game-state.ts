export interface Move {
  moveNumber: number;
  player: number;
  row: number;
  column: number;
}

export interface GameState {
  id: string;
  board: string[][];
  currentPlayer: number;
  mode: number;
  status: number;
  winner?: number;
  winningCells: number[];
  moveHistory: Move[];
}