import { BrowserModule } from '@angular/platform-browser';
import {APP_INITIALIZER, NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from '@angular/material/form-field';
import {HttpClientModule} from '@angular/common/http';
import {ToastrModule} from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { HeaderComponent } from './header/header.component';
import { TheatersComponent } from './theaters/theaters.component';
import {BasicServicesModule, PersistenceService} from 'basic-services';

import {environment} from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TheatersComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    FlexLayoutModule,
    ToastrModule.forRoot(),
    BasicServicesModule.forRoot(environment),
    AppRoutingModule
  ],
  providers: [
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'fill' }
    },
    {
      provide: APP_INITIALIZER,
      useFactory: (persistenceService: PersistenceService) => () => persistenceService.connect(),
      deps: [PersistenceService],
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
