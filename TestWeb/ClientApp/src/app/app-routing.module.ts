import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { environment } from 'src/environments/environment';
import { CreateHearingComponent } from './create-hearing/create-hearing.component';
import { PageUrls } from './shared/page-url.constants';

export const routes: Routes = [
    { path: '', redirectTo: `${PageUrls.CreateHearings}`, pathMatch: 'full' },
    { path: PageUrls.Home, redirectTo: `${PageUrls.CreateHearings}`, pathMatch: 'full' },
    { path: `${PageUrls.CreateHearings}`, component: CreateHearingComponent },
    { path: '**', redirectTo: `${PageUrls.NotFound}`, pathMatch: 'full' }
];

@NgModule({
    exports: [RouterModule],
    imports: [RouterModule.forRoot(routes, { enableTracing: !environment.production })]
})
export class AppRoutingModule {}
