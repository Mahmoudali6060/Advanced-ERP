import { NgModule } from '@angular/core';
import { MatTreeModule } from "@angular/material/tree";
import { MatIconModule } from "@angular/material/icon";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatButtonModule } from "@angular/material/button";

@NgModule({
    imports: [
        MatTreeModule,
        MatIconModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatButtonModule
    ],
    exports: [
        MatTreeModule,
        MatIconModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatButtonModule
    ]
})
export class MaterialModule { }