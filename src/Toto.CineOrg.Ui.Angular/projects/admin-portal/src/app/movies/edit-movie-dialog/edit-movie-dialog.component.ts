import {Component, Inject, OnInit} from '@angular/core';
import {MovieForUpdate} from '../../models/movie';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {Observable} from 'rxjs';

@Component({
  templateUrl: './edit-movie-dialog.component.html',
  styleUrls: ['./edit-movie-dialog.component.scss']
})
export class EditMovieDialogComponent implements OnInit {

  public movie: MovieForUpdate = {} as MovieForUpdate;
  public genres$: Observable<string[]>;

  constructor(private dialogRef: MatDialogRef<EditMovieDialogComponent>, @Inject(MAT_DIALOG_DATA) private data: any) {
    this.genres$ = data.genres$;
    this.movie = data.movie;
  }

  ngOnInit(): void {
    this.dialogRef
      .afterClosed()
      .subscribe(result => console.log(`Add Movie Dialog was closed. Result: ${JSON.stringify(result)}`));
  }

  public onCancel(): void {
    this.dialogRef.close(null);
  }

  public onSuccess(): void {
    this.dialogRef.close(this.movie);
  }

}
