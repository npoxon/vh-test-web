import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdalService } from 'adal-angular4';
import { ConfigService } from './services/api/config.service';
import { ErrorService } from './services/error.service';
import { LocationService } from './services/location.service';
import { Logger } from './services/logging/logger-base';
import { PageUrls } from './shared/page-url.constants';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
    private readonly loggerPrefix = '[App] -';
    loggedIn: boolean;

    constructor(
        private adalService: AdalService,
        private configService: ConfigService,
        private router: Router,
        private locationService: LocationService,
        private errorService: ErrorService,
        private logger: Logger
    ) {
        this.loggedIn = false;
        this.initAuthentication();
    }

    ngOnInit() {
        this.logger.debug(`${this.loggerPrefix} Starting app. Checking Auth`, this.configService.getClientSettings());
        this.checkAuth();
    }

    private initAuthentication() {
        const clientSettings = this.configService.getClientSettings();
        const config = {
            tenant: clientSettings.tenant_id,
            clientId: clientSettings.client_id,
            postLogoutRedirectUri: clientSettings.post_logout_redirect_uri,
            redirectUri: clientSettings.redirect_uri,
            cacheLocation: 'sessionStorage'
        };
        this.adalService.init(config);
    }

    async checkAuth(): Promise<void> {
        const currentUrl = this.locationService.getCurrentUrl();
        if (this.locationService.getCurrentPathName() !== `/${PageUrls.Logout}`) {
            this.adalService.handleWindowCallback();
            this.loggedIn = this.adalService.userInfo.authenticated;
            if (!this.loggedIn) {
                this.router.navigate([`/${PageUrls.Login}`], { queryParams: { returnUrl: currentUrl } });
                return;
            }
            if (!this.isUserAVhQA()) {
                this.errorService.goToUnauthorised();
            }
        }
    }

    isUserAVhQA(): boolean {
        if (this.adalService.userInfo.profile.roles) {
            return (this.adalService.userInfo.profile.roles as string[]).includes('VHQA');
        }
        return false;
    }

    logOut() {
        this.loggedIn = false;
        sessionStorage.clear();
        this.adalService.logOut();
    }
}
