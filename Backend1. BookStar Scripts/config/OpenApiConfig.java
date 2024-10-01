package com.CoRangE.BookStar.config;

import io.swagger.v3.oas.annotations.OpenAPIDefinition;
import io.swagger.v3.oas.annotations.info.Contact;
import io.swagger.v3.oas.annotations.info.Info;
import io.swagger.v3.oas.annotations.info.License;
import io.swagger.v3.oas.annotations.servers.Server;

@OpenAPIDefinition(
        info = @Info(
                contact = @Contact(
                        name = "Wade",
                        email = "astrsy36@gmail.com",
                        url = ""
                ),
                description = "description",
                title = "Test",
                version = "0.0.1",
                license = @License(
                        name = "Licence name",
                        url = "https://test-url.com"
                ),
                termsOfService = "Terms of service"
        ),
        servers = {
                @Server(
                        description = "Local Env",
                        url = "http://localhost:8081"
                )
        }

)
public class OpenApiConfig {
}
