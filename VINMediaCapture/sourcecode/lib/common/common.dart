import 'dart:io';

const String apiUrl = "https://10.0.2.2:7262/api/";
const String hostUrl = "https://10.0.2.2:7015/";
Future<void> deleteFile(String filePath) async {
  try {
    var file = new File(filePath);
    if (await file.exists()) {
      await file.delete();
    }
  } catch (e) {
    // Error in getting access to the file.
  }
}
