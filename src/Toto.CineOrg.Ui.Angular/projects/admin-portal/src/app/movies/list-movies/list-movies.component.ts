import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Movie} from '../../models';

@Component({
  selector: 'app-list-movies',
  templateUrl: './list-movies.component.html',
  styleUrls: ['./list-movies.component.scss']
})
export class ListMoviesComponent {

  public displayedColumns: string[] = ['title', 'genre', 'yearReleased', 'edit', 'delete'];

  @Input() movies: Movie[] = [];

  @Output('item-add') itemAdd = new EventEmitter();
  @Output('item-edit') itemEdit = new EventEmitter();
  @Output('item-delete') itemDelete = new EventEmitter();

  OnAdd() {
    this.itemAdd.emit();
  }

  onEdit(id: any) {
    this.itemEdit.emit(id);
  }

  onDelete(id: any) {
    this.itemDelete.emit(id);
  }
}
