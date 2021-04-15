import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Movie} from '../../models/movie';

@Component({
  templateUrl: './delete-movie-dialog.component.html',
  styleUrls: ['./delete-movie-dialog.component.scss']
})
export class DeleteMovieDialogComponent implements OnInit {

  public movie: Movie = {} as Movie;

  constructor(private dialogRef: MatDialogRef<DeleteMovieDialogComponent>, @Inject(MAT_DIALOG_DATA) private data: any) {
    this.movie = data.movie;
  }

  ngOnInit(): void {
    this.dialogRef
      .afterClosed()
      .subscribe(result => console.log(`Delete movie Dialog was closed. Result: ${JSON.stringify(result)}`));
  }

  public onCancel(): void {
    this.dialogRef.close(null);
  }

  public onSuccess(): void {
    this.dialogRef.close(this.movie);
  }

}
