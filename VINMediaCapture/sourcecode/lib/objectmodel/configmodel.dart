import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class ConfigModel {
  String hostUrl;
  String apiUrl;
  ConfigModel(this.hostUrl, this.apiUrl);

  Map<String, dynamic> toJson() {
    return {
      'hostUrl': hostUrl,
      'apiUrl': apiUrl,
    };
  }

  factory ConfigModel.fromJson(Map<String, dynamic> json) {
    return new ConfigModel(json['hostUrl'], json['apiUrl']);
  }
}
