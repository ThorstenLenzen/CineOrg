import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {MovieForCreate} from '../../models/movie';
import {Observable} from 'rxjs';

@Component({
  templateUrl: './add-movie-dialog.component.html',
  styleUrls: ['./add-movie-dialog.component.scss']
})
export class AddMovieDialogComponent implements OnInit {

  public movie: MovieForCreate = {} as MovieForCreate;
  public genres$: Observable<string[]>;

  constructor(private dialogRef: MatDialogRef<AddMovieDialogComponent>, @Inject(MAT_DIALOG_DATA) private data: any) {
    this.genres$ = data.genres$;
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
