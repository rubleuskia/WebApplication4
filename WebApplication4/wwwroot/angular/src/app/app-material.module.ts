import { NgModule } from '@angular/core';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
    imports: [
        MatSliderModule,
        MatCardModule,
        MatButtonModule,
    ],
    exports: [
        MatSliderModule,
        MatCardModule,
        MatButtonModule,
    ]
})
export class AppMaterialModule { }
