import {ModuleWithProviders, NgModule} from '@angular/core';

@NgModule({
  declarations: [],
  imports: [],
  exports: []
})
export class BasicServicesModule {
  static forRoot(environment: any): ModuleWithProviders {
    return {
      ngModule: BasicServicesModule,
      providers: [
        { provide: 'environment', useValue: environment },
      ]
    };
  }
}
